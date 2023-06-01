import { useEffect, useState } from "react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import NavBar from "./components/NavBar.tsx";
import { PasswordGenerator } from "./utils/PasswordGenerator.ts";
import GeneratePasswordDto from "./types/GeneratePasswordDto.ts";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import AccountsPage from "./pages/AccountsPage.tsx";
import { ToastContainer } from "react-toastify";
import { Container } from "semantic-ui-react";
import PasswordGeneratorPage from "./pages/PasswordGeneratorPage.tsx";

function App() {
  return (
    <>
      <ToastContainer />
      <NavBar />
      <Container className="App">
        <Routes>
          <Route path="/" element={<PasswordGeneratorPage />} />
          <Route path="/accounts" element={<AccountsPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </Container>
    </>
  );
}

export default App;
