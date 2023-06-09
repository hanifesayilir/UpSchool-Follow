import { useEffect, useState } from "react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import AccountsPage from "./pages/AccountsPage.tsx";
import { ToastContainer } from "react-toastify";
import { Container } from "semantic-ui-react";
import PasswordGeneratorPage from "./pages/PasswordGeneratorPage.tsx";
import {LocalJwt, LocalUser} from "./types/AuthTypes.ts";
import LoginPage from "./pages/LoginPage.tsx";
import { AccountGetAllDto } from "./types/AccountTypes.ts";
import NavBar from "./components/NavBar.tsx";
import { useNavigate} from "react-router-dom"
import {getClaimsFromJwt} from "./utils/JwtHelper";


/*const dummyAccounts: AccountGetAllDto[] = [
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
];*/

function App() {

    const navigate = useNavigate();
  const [accounts, setAccounts] = useState<AccountGetAllDto[]>([]);

  const [appUser, setAppUser] = useState<LocalUser | undefined>(undefined);

  useEffect( () =>{
      const jwtJson  = localStorage.getItem("upstorage_user");
      if(!jwtJson) {
        navigate("/login");
        return;
      }
      const localJwt : LocalJwt = JSON.parse(jwtJson);

      const expires : string = localJwt.expires;
      const { uid, email, given_name, family_name} = getClaimsFromJwt(localJwt.accessToken);
      setAppUser({ id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken: localJwt.accessToken });



  },[])

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
