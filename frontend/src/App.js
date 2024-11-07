import React, { useState, useEffect } from "react";
import "./App.css";
import { ResponsiveAppBar } from "./components/Header/Header";
import { Expenses } from "./components/Expenses/Expenses";
import Login from "./components/Login/Login";
import { Incomes } from "./components/Incomes/Incomes";
import ReportGrid from "./components/ReportGrid/ReportGrid";

function App() {
  const [pages, setPages] = useState([
    { name: "Трошоци", active: true },
    { name: "Приходи", active: false },
    { name: "Извештај", active: false },
  ]);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setIsAuthenticated(true);
    }
  }, []);

  return (
    <>
      <ResponsiveAppBar pages={pages} setPages={setPages} />
      {isAuthenticated &&
        pages.find((page) => page.name === "Трошоци").active && <Expenses />}
      {isAuthenticated &&
        pages.find((page) => page.name === "Приходи").active && <Incomes />}
      {isAuthenticated &&
        pages.find((page) => page.name === "Извештај").active && <ReportGrid />}
      {!isAuthenticated && <Login />}
    </>
  );
}

export default App;
