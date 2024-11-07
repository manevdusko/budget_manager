import React, { useEffect, useState } from "react";
import Report from "../Report/Report";
import axios from "axios";
import NewRecord from "../NewRecord/NewRecord";

export const Incomes = () => {
  const [records, setRecords] = useState([]);

  const loadRecords = () => {
    const token = localStorage.getItem("token");
    axios
      .get("http://localhost:5187/api/income", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => setRecords(response.data))
      .catch((error) => console.error("Error fetching records:", error));
  };

  useEffect(() => {
    loadRecords();
  }, []);

  return (
    <div style={{ margin: "20px" }}>
      <h2>Додади приход</h2>
      <NewRecord onExpenseAdded={loadRecords} incomeOrExpense={'income'} />
      <h2>Извештај</h2>
      <Report
        records={records}
        loadRecords={loadRecords}
        incomeOrExpense={'income'}
      />
    </div>
  );
};
