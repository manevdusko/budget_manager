import React, { useState, useEffect } from "react";
import {
  TextField,
  Button,
  MenuItem,
  Autocomplete,
  Box,
  Stack,
} from "@mui/material";
import axios from "axios";

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
const currentMonth = months[new Date().getMonth()];
const currentYear = new Date().getFullYear();
const years = Array.from(
  { length: 2035 - currentYear + 1 },
  (_, i) => currentYear + i
);

const NewRecord = ({ onExpenseAdded, incomeOrExpense }) => {
  const [categories, setCategories] = useState([]);
  const [type, setType] = useState([]);
  const [formData, setFormData] = useState({
    month: currentMonth,
    year: currentYear,
    category: "",
    amount: "0.00",
    valueType: "planned",
  });

  useEffect(() => {
    const token = localStorage.getItem("token");
    axios
      .get(`http://localhost:5187/api/${incomeOrExpense}-category`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => setCategories(response.data))
      .catch((error) => console.error("Error fetching categories:", error));

    axios
      .get("http://localhost:5187/api/type", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => setType(response.data))
      .catch((error) => console.error("Error fetching categories:", error));
  }, []);

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (event) => {
    const token = localStorage.getItem("token");

    event.preventDefault();

    axios
      .post(
        `http://localhost:5187/api/${incomeOrExpense}`,
        {
          month: formData.month.toString(),
          year: formData.year.toString(),
          categoryId: formData.category,
          amount: parseFloat(formData.amount),
          typeId: formData.valueType,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      )
      .then((response) => {
        onExpenseAdded();
        setFormData({
          month: currentMonth,
          year: currentYear,
          category: "",
          amount: "0.00",
          valueType: "planned",
        });
      })
      .catch((error) => console.error("Error saving data:", error));
  };

  const handleCategoryChange = (event) => {
    const { value } = event.target;
    setFormData({ ...formData, category: value });
  };

  const handleTypeChange = (event) => {
    const { value } = event.target;
    setFormData({ ...formData, valueType: value });
  };

  return (
    <Box
      component="form"
      autoComplete="off"
      onSubmit={handleSubmit}
      sx={{ flexGrow: 1, padding: 2 }}
    >
      <Stack spacing={2}>
        <Autocomplete
          options={months}
          value={formData.month}
          onChange={(event, newValue) =>
            setFormData({ ...formData, month: newValue })
          }
          renderInput={(params) => (
            <TextField
              {...params}
              label="Месец"
              variant="outlined"
              color="secondary"
              fullWidth
              required
            />
          )}
        />
        <Autocomplete
          options={years}
          value={formData.year}
          onChange={(event, newValue) =>
            setFormData({ ...formData, year: newValue })
          }
          renderInput={(params) => (
            <TextField
              {...params}
              label="Година"
              variant="outlined"
              color="secondary"
              fullWidth
              required
            />
          )}
        />
        <TextField
          select
          label="Категорија"
          name="category"
          value={formData.category}
          onChange={handleCategoryChange}
          variant="outlined"
          color="secondary"
          fullWidth
          required
        >
          {categories.map((category) => (
            <MenuItem key={category.id} value={category.id}>
              {category.categoryName}
            </MenuItem>
          ))}
        </TextField>
        <TextField
          label="Износ"
          name="amount"
          type="number"
          slotProps={{ htmlInput: { step: "0.01" } }}
          value={formData.amount}
          onChange={handleChange}
          variant="outlined"
          color="secondary"
          fullWidth
          required
        />
        <TextField
          select
          label="Тип на вредност"
          name="type"
          value={formData.valueType}
          onChange={handleTypeChange}
          variant="outlined"
          color="secondary"
          fullWidth
          required
        >
          {type.map((type) => (
            <MenuItem key={type.id} value={type.id}>
              {type.typeName}
            </MenuItem>
          ))}
        </TextField>
        <Button variant="outlined" color="secondary" type="submit">
          Зачувај
        </Button>
      </Stack>
    </Box>
  );
};

export default NewRecord;
