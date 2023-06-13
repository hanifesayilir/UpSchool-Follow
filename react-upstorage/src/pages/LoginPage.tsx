import React, {useContext, useState} from "react";
import {AuthLoginCommand, LocalJwt, LocalUser} from "../types/AuthTypes";
import {
  Button,
  Form,
  Grid,
  Header,
  Icon,
  Image,
  Segment,
} from "semantic-ui-react";
import axios from "axios";
import {getClaimsFromJwt} from "../utils/JwtHelper";
import {findAllInRenderedTree} from "react-dom/test-utils";
import { useNavigate} from "react-router-dom"
import {toast} from "react-toastify";
import {AppUserContext} from "../context/StateContext";
import api from  "../utils/AxiosInstance"

/*export type LoginPageProps = {
  setAppUser: (appUser: LocalUser) => void;
};*/



function LoginPage() {

  const navigate = useNavigate();

  const { setAppUser} = useContext(AppUserContext);

  const [authLoginCommand, setAuthLoginCommand] = useState<AuthLoginCommand>({
    email: "",
    password: "",
  });

  const handleSubmit = async (event: React.FormEvent) => {

    event.preventDefault();

    try {
      const response = await api.post("/Authentication/Login", authLoginCommand);

      if (response.status === 200) {
        const accessToken = response.data.accessToken;
        const {uid, email, given_name, family_name} = getClaimsFromJwt(accessToken);
        const expires: string = response.data.expires;

        setAppUser({ id:uid, email, firstName:given_name, lastName:family_name, expires, accessToken });

        const localJwt: LocalJwt = {
          accessToken,
          expires
        }

        localStorage.setItem("upstorage_user", JSON.stringify(localJwt));
        navigate("/");


      } else {
        toast.error(response.statusText)
      }
    } catch (error) {
      toast.error("Something went wrong!");
    }
  };
  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAuthLoginCommand({
      ...authLoginCommand,
      [event.target.name]: event.target.value
    });
  }

  const onGoogleLoginClick = (e: React.FormEvent) => {
    // Handle Google login
    e.preventDefault();
  };

  return (
    <Grid textAlign="center" style={{ height: "100vh" }}>
      <Grid.Column style={{ maxWidth: 450 }}>
        <Image
          src="/upstorage_logo_730_608.png"
          size="medium"
          centered
          style={{ marginTop: "1em" }}
        />
        <Header as="h2" color="teal" textAlign="center">
          Log-in to your account
        </Header>
        <Form size="large" onSubmit={handleSubmit}>
          <Segment stacked>
            <Form.Input
              fluid
              icon="mail"
              iconPosition="left"
              placeholder="Email"
              value={authLoginCommand.email}
              onChange={handleInputChange}
              name="email"
            />
            <Form.Input
              fluid
              icon="lock"
              iconPosition="left"
              placeholder="Password"
              type="password"
              value={authLoginCommand.password}
              onChange={handleInputChange}
              name="password"
            />

            <Button color="teal" fluid size="large" type="submit">
              Login
            </Button>

            <Button
              color="red"
              fluid
              onClick={(e) => onGoogleLoginClick(e)}
              size="large"
              style={{ marginTop: "5px" }}
            >
              <Icon name="google" /> Sign in with Google
            </Button>
          </Segment>
        </Form>
      </Grid.Column>
    </Grid>
  );
}

export default LoginPage;
