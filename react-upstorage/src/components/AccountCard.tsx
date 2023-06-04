
import React from 'react'
import { AccountGetAllDto } from '../types/AccountTypes';
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


export type AccountCardProps ={
    account: AccountGetAllDto,
    index: number,
    onPasswordVisibilityToggle: (Id: string) =>void,
    onDeleteButtonClick:(Id:string) =>void,
    onEditButtonClick:(Id: string) =>void,
}

function AccountCard({account, index, onDeleteButtonClick, onEditButtonClick, onPasswordVisibilityToggle}:AccountCardProps) {
  return (
   <Grid.Column key={index}>
            <Card color={index % 2 === 0 ? "blue" : "red"} raised>
              <Card.Content header={account.Title} textAlign="center" />
              <Card.Content>
                <Input
                  type="text"
                  value={account.UserName}
                  textAlign="center"
                />
                <Input
                  icon={{
                    name: account.ShowPassword ? "eye slash outline" : "eye",
                    link: true,
                    onClick: () => onPasswordVisibilityToggle(account.Id),
                  }}
                  type={account.ShowPassword ? "text" : "password"}
                  value={account.Password}
                  textAlign="center"
                />
              </Card.Content>
              <Card.Content extra>
                <div className="ui two buttons">
                  <Button
                    basic
                    color="blue"
                    onClick={() => onEditButtonClick(account.Id)}
                  >
                    <Icon name="edit" /> Edit
                  </Button>
                  <Button
                    basic
                    color="red"
                    onClick={() => onDeleteButtonClick(account.Id)}
                  >
                    <Icon name="delete" /> Delete
                  </Button>
                </div>
              </Card.Content>
            </Card>
          </Grid.Column>
}
export default AccountCard;