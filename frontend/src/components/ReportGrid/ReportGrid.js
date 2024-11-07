import React, { useState, useEffect } from 'react';
import { Box } from '@mui/material';
import axios from 'axios';

const ReportGrid = () => {
  const [incomes, setIncomes] = useState([]);
  const [expenses, setExpenses] = useState([]);
  const [types, setTypes] = useState([]);
  const [uniqueMonthsYears, setUniqueMonthsYears] = useState([]);

  useEffect(() => {
    const token = localStorage.getItem('token');

    const fetchIncomes = axios.get('http://localhost:5187/api/income', {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });

    const fetchExpenses = axios.get('http://localhost:5187/api/expense', {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });

    const fetchTypes = axios.get('http://localhost:5187/api/type', {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });

    Promise.all([fetchIncomes, fetchExpenses, fetchTypes])
      .then(([incomeResponse, expenseResponse, typeResponse]) => {
        setIncomes(incomeResponse.data);
        setExpenses(expenseResponse.data);
        setTypes(typeResponse.data);

        const combinedData = [...incomeResponse.data, ...expenseResponse.data];
        const uniqueMonthsYearsSet = new Set(
          combinedData.map((entry) => `${entry.month} ${entry.year}`)
        );
        setUniqueMonthsYears(Array.from(uniqueMonthsYearsSet));
      })
      .catch((error) => console.error("Error fetching records:", error));
  }, []);

  const getCategoryValues = (category, data) => {
    const values = {};
    uniqueMonthsYears.forEach(monthYear => {
      values[monthYear] = {};
      types.forEach(type => {
        values[monthYear][type.typeName] = 0;
      });
    });

    data.forEach(entry => {
      if (entry.category.categoryName === category) {
        const monthYear = `${entry.month} ${entry.year}`;
        values[monthYear][entry.type.typeName] = entry.amount;
      }
    });

    return values;
  };

  const getTotalValues = (data) => {
    const totals = {};
    uniqueMonthsYears.forEach(monthYear => {
      totals[monthYear] = {};
      types.forEach(type => {
        totals[monthYear][type.typeName] = 0;
      });
    });

    data.forEach(entry => {
      const monthYear = `${entry.month} ${entry.year}`;
      totals[monthYear][entry.type.typeName] += entry.amount;
    });

    return totals;
  };

  const incomeCategories = Array.from(new Set(incomes.map(entry => entry.category.categoryName)));
  const expenseCategories = Array.from(new Set(expenses.map(entry => entry.category.categoryName)));

  const incomeTotals = getTotalValues(incomes);
  const expenseTotals = getTotalValues(expenses);

  const overallTotals = {};
  uniqueMonthsYears.forEach(monthYear => {
    overallTotals[monthYear] = {};
    types.forEach(type => {
      overallTotals[monthYear][type.typeName] = incomeTotals[monthYear][type.typeName] + expenseTotals[monthYear][type.typeName];
    });
  });

  return (
    <Box sx={{ height: 400, width: "100%" }}>
      <table>
        <thead>
          <tr>
            <th>Category</th>
            {uniqueMonthsYears.length > 0 &&
              uniqueMonthsYears.map((monthYear, index) => (
                <th key={index} colSpan={types.length}>{monthYear}</th>
              ))}
          </tr>
          <tr>
            <th></th>
            {uniqueMonthsYears.length > 0 &&
              uniqueMonthsYears.map((monthYear, index) => (
                types.map((type, typeIndex) => (
                  <th key={`${index}-${typeIndex}`}>{type.typeName}</th>
                ))
              ))}
          </tr>
        </thead>
        <tbody>
          {incomeCategories.map((category, index) => {
            const incomeValues = getCategoryValues(category, incomes);
            return (
              <React.Fragment key={index}>
                <tr>
                  <td>{category} (Income)</td>
                  {uniqueMonthsYears.map((monthYear, index) => (
                    types.map((type, typeIndex) => (
                      <td key={`${index}-${typeIndex}`}>
                        {incomeValues[monthYear][type.typeName]}
                      </td>
                    ))
                  ))}
                </tr>
              </React.Fragment>
            );
          })}
          <tr>
            <td><b>Вкупно приходи</b></td>
            {uniqueMonthsYears.map((monthYear, index) => (
              types.map((type, typeIndex) => (
                <td key={`${index}-${typeIndex}`}>
                  <b>{incomeTotals[monthYear][type.typeName]}</b>
                </td>
              ))
            ))}
          </tr>
          {expenseCategories.map((category, index) => {
            const expenseValues = getCategoryValues(category, expenses);
            return (
              <React.Fragment key={index}>
                <tr>
                  <td>{category} (Expense)</td>
                  {uniqueMonthsYears.map((monthYear, index) => (
                    types.map((type, typeIndex) => (
                      <td key={`${index}-${typeIndex}`}>
                        {expenseValues[monthYear][type.typeName]}
                      </td>
                    ))
                  ))}
                </tr>
              </React.Fragment>
            );
          })}
          <tr>
            <td><b>Вкупно трошоци</b></td>
            {uniqueMonthsYears.map((monthYear, index) => (
              types.map((type, typeIndex) => (
                <td key={`${index}-${typeIndex}`}>
                  <b>{expenseTotals[monthYear][type.typeName]}</b>
                </td>
              ))
            ))}
          </tr>
          <tr>
            <td><b>Салдо</b></td>
            {uniqueMonthsYears.map((monthYear, index) => (
              types.map((type, typeIndex) => (
                <td key={`${index}-${typeIndex}`}>
                  <b>{overallTotals[monthYear][type.typeName]}</b>
                </td>
              ))
            ))}
          </tr>
        </tbody>
      </table>
    </Box>
  );
};

export default ReportGrid;