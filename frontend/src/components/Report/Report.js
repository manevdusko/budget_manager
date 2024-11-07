import React, { useState, useEffect } from "react";
import {
  Box,
  Button,
  TextField,
  IconButton,
  Autocomplete,
  Stack,
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { Edit, Delete } from "@mui/icons-material";
import axios from "axios";

const currentYear = new Date().getFullYear();
const months = [
  "Јануари",
  "Февруари",
  "Март",
  "Април",
  "Мај",
  "Јуни",
  "Јули",
  "Август",
  "Септември",
  "Октомври",
  "Ноември",
  "Декември",
];
const years = Array.from(
  { length: 2035 - currentYear + 1 },
  (_, i) => currentYear + i
);

const Report = ({ records, loadRecords, incomeOrExpense }) => {
  const [editId, setEditId] = useState(null);
  const [editData, setEditData] = useState({ month: "", year: "", amount: "" });
  const token = localStorage.getItem("token");

  const handleEditClick = (id) => {
    const record = records.find((record) => record.id === id);
    setEditId(id);
    setEditData({
      month: record.month,
      year: record.year,
      amount: record.amount,
    });
  };

  const handleDeleteClick = (id) => {
    axios
      .delete(`http://localhost:5187/api/${incomeOrExpense}/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then(() => {
        loadRecords();
      })
      .catch((error) => console.error("Error deleting record:", error));
  };

  const handleSaveClick = () => {
    axios
      .put(`http://localhost:5187/api/${incomeOrExpense}/${editId}`, editData, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        loadRecords();
        setEditId(null);
      })
      .catch((error) => console.error("Error saving record:", error));
  };

  const handleCancelClick = () => {
    setEditId(null);
  };

  const handleChange = (event) => {
    const { name, value } = event.target;
    setEditData({ ...editData, [name]: value });
  };

  const columns = [
    { field: "month", headerName: "Месец", width: 150, editable: true },
    { field: "year", headerName: "Година", width: 150, editable: true },
    {
      field: "category",
      headerName: "Категорија",
      width: 150,
      editable: true,
      valueGetter: (params) => params.categoryName,
    },
    { field: "amount", headerName: "Износ", width: 150, editable: true },
    {
      field: "type",
      headerName: "Тип",
      width: 150,
      editable: true,
      valueGetter: (params) => params.typeName,
    },
    {
      field: "actions",
      headerName: "Опции",
      width: 150,
      renderCell: (params) => (
        <>
          <IconButton onClick={() => handleEditClick(params.id)}>
            <Edit />
          </IconButton>
          <IconButton onClick={() => handleDeleteClick(params.id)}>
            <Delete />
          </IconButton>
        </>
      ),
    },
  ];

  return (
    <Box sx={{ height: 400, width: "100%" }}>
      <DataGrid rows={records} columns={columns} pageSize={5} />
      {editId && (
        <Box sx={{ mt: 2 }}>
          <Stack spacing={2} direction="row">
            <Autocomplete
              options={months}
              value={editData.month}
              onChange={(event, newValue) =>
                setEditData({ ...editData, month: newValue })
              }
              renderInput={(params) => <TextField {...params} label="Месец" />}
              fullWidth
              required
            />
            <Autocomplete
              options={years}
              value={editData.year}
              onChange={(event, newValue) =>
                setEditData({ ...editData, year: newValue })
              }
              renderInput={(params) => <TextField {...params} label="Година" />}
              fullWidth
              required
            />
            <TextField
              label="Износ"
              name="amount"
              type="number"
              inputProps={{ step: "0.01" }}
              value={editData.amount}
              onChange={handleChange}
              fullWidth
              required
            />
            <Button
              variant="contained"
              color="primary"
              onClick={handleSaveClick}
            >
              Save
            </Button>
            <Button
              variant="contained"
              color="secondary"
              onClick={handleCancelClick}
            >
              Cancel
            </Button>
          </Stack>
        </Box>
      )}
    </Box>
  );
};

export default Report;
