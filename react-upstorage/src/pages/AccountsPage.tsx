import React, { useEffect, useState } from "react";
import { AccountGetAllDto } from "../types/AccountTypes";
import "./AccountsPage.css";
import {
  Button,
  Divider,
  Grid,
  Header,
  Icon,
  Input,
  Segment,
  Select,
  Card,
} from "semantic-ui-react";
import AccountCard from "../components/AccountCard";

function AccountsPage() {
  const dummyAccounts: AccountGetAllDto[] = [
    {
      Id: "12345",
      Title: "Yemek Sepeti",
      Url: "www.emeksepeti",
      IsFavourite: false,
      UserId: "ankgmial.com",
      UserName: "alpettunga",
      Password: "123alper32",
      Categories: [],
      ShowPassword: false,
    },

    {
      Id: "45866",
      Title: "Getir",
      Url: "www.getir.com",
      IsFavourite: false,
      UserId: "ankgmial.com",
      UserName: "alpettunga",
      Password: "123alper32",
      Categories: [],
      ShowPassword: false,
    },
  ];
  const [accounts, setAccounts] = useState<AccountGetAllDto[]>(dummyAccounts);

  const options = [
    { key: "1", text: "Ascending", value: "true" },
    { key: "2", text: "Descending", value: "false" },
  ];

  const onPasswordVisibilityToggle = (id: string) => {
    // Create a new array with the same accounts, but with the showPassword property of the account with the given id toggled
    const updatedAccounts = accounts.map((account) => {
      if (account.Id === id) {
        // Toggle the showPassword property
        return { ...account, ShowPassword: !account.ShowPassword };
      } else {
        // Leave the account unchanged
        return account;
      }
    });

    // Update the state with the new array
    setAccounts(updatedAccounts);
  };

  const onAddButtonClick = () => {
    console.log("Add button clicked");
  };

  const onSearchInputChange = () => {
    console.log("Search input changed");
  };

  const onSelectChange = () => {
    console.log("Select option changed");
  };

  const onEditButtonClick = (id: string) => {
    console.log(`Edit button clicked for id: ${id}`);
  };

  const onDeleteButtonClick = (id: string) => {
    console.log(`Delete button clicked for id: ${id}`);
  };

  useEffect(() => {
    return;
  }, []);

  return (
    <Segment padded="very">
      <Header as="h1" textAlign="center" className="main-header">
        My Accounts
      </Header>
      <div className="ui fitted segment d-flex justify-center">
        <Button color="green" onClick={onAddButtonClick}>
          <Icon name="add circle" /> Add
        </Button>
        <Input
          className="ml-2"
          icon="search"
          placeholder="Search"
          onChange={onSearchInputChange}
        />
        <Select
          className="ml-2"
          placeholder="Select order"
          options={options}
          onChange={onSelectChange}
        />
      </div>
      <Divider section />
      <Grid columns={3} stackable>
        {accounts.map((account, index) => (
          <AccountCard
            account={account}
            index={index}
            onPasswordVisibilityToggle={onPasswordVisibilityToggle}
            onDeleteButtonClick={onDeleteButtonClick}
            onEditButtonClick={onEditButtonClick}
          />
        ))}
      </Grid>
    </Segment>
  );
}

export default AccountsPage;
