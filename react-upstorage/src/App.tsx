import { useEffect, useState } from "react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import AccountsPage from "./pages/AccountsPage.tsx";
import { ToastContainer } from "react-toastify";
import { Container } from "semantic-ui-react";
import PasswordGeneratorPage from "./pages/PasswordGeneratorPage.tsx";
import { LocalUser } from "./types/AuthTypes.ts";
import LoginPage from "./pages/LoginPage.tsx";
import { AccountGetAllDto } from "./types/AccountTypes.ts";
import NavBar from "./components/NavBar.tsx";

const dummyAccounts: AccountGetAllDto[] = [
  {
    id: "12345",
    title: "Yemek Sepeti",
    url: "www.emeksepeti",
    isFavourite: false,
    userId: "ankgmial.com",
    userName: "alpettunga",
    password: "123alper32",
    categories: [],
    showPassword: false,
  },

  {
    id: "45866",
    title: "Getir",
    url: "www.getir.com",
    isFavourite: false,
    userId: "ankgmial.com",
    userName: "alpettunga",
    password: "123alper32",
    categories: [],
    showPassword: false,
  },
];

function App() {
  const [accounts, setAccounts] = useState<AccountGetAllDto[]>(dummyAccounts);

  const [appUser, setAppUser] = useState<LocalUser | undefined>(undefined);

  return (
    <>
      <ToastContainer />
      <NavBar accounts={accounts} appUser={appUser} />
      <Container className="App">
        <Routes>
          <Route path="/" element={<PasswordGeneratorPage />} />
          <Route
            path="/accounts"
            element={
              <AccountsPage accounts={accounts} setAccounts={setAccounts} />
            }
          />
          <Route
            path="/login"
            element={<LoginPage setAppUser={setAppUser} />}
          />
          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </Container>
    </>
  );
}

export default App;
