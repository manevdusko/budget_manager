import React, { useEffect, useState } from "react";
import Report from "../Report/Report";
import axios from "axios";
import NewRecord from "../NewRecord/NewRecord";

export const Expenses = () => {
  const [records, setRecords] = useState([]);

  const loadRecords = () => {
    const token = localStorage.getItem("token");
    axios
      .get("http://localhost:5187/api/expense", {
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
      <h2>Додади трошок</h2>
      <NewRecord onExpenseAdded={loadRecords} incomeOrExpense={"expense"} />
      <h2>Извештај</h2>
      <Report
        records={records}
        incomeOrExpense={"expense"}
        loadRecords={loadRecords}
      />
    </div>
  );
};
