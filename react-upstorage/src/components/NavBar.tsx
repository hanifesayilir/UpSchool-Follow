import { Container, Menu, Image } from "semantic-ui-react";
import { NavLink } from "react-router-dom";
import { AccountGetAllDto } from "../types/AccountTypes";

export type NabBarProps = {
  accounts: AccountGetAllDto;
};

const NavBar = () => {
  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item as="a" header>
          <Image size="mini" src="/vite.svg" style={{ marginRight: "1.5em" }} />
          UpStorage
        </Menu.Item>
        <Menu.Item as={NavLink} to="/">
          {" "}
          Home
        </Menu.Item>
        <Menu.Item as={NavLink} to="/accounts">
          Accounts
        </Menu.Item>
        <Menu.Item as={NavLink} to="/dafa">
          Not Found
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default NavBar;
