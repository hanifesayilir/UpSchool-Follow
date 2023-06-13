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
import {AppUserContext, AccountsContext} from "./context/StateContext";
import {DummyData} from "./utils/DummyData";
import ProtectedRoute from "./components/ProtectedRoute";


function App() {
    const navigate = useNavigate();
  const [accounts, setAccounts] = useState<AccountGetAllDto[]>(DummyData);

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
        <AppUserContext.Provider value={{appUser, setAppUser}}>
            <AccountsContext.Provider value={{ accounts, setAccounts}}>
                <ToastContainer />
                <NavBar />
                <Container className="App">
                    <Routes>
                        <Route path="/" element={
                            <ProtectedRoute>
                                <PasswordGeneratorPage />
                            </ProtectedRoute>

                           } />
                        <Route path="/accounts" element={
                            <ProtectedRoute>
                                <AccountsPage />
                            </ProtectedRoute>
                        }/>
                        <Route path="/login" element={<LoginPage />}/>
                        <Route path="*" element={<NotFoundPage />} />
                    </Routes>
                </Container>
            </AccountsContext.Provider>
        </AppUserContext.Provider>
    </>
  );
}

export default App;
