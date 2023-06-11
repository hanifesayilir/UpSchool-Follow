import React, { createContext} from "react"
import {LocalUser} from "../types/AuthTypes";
import {AccountGetAllDto} from "../types/AccountTypes";

export type AppUserContextType = {
    appUser: LocalUser | undefined,
    setAppUser: React.Dispatch<React.SetStateAction<LocalUser | undefined>>
}

export type AccountsContextType = {
    accounts:  AccountGetAllDto[],
    setAccounts: React.Dispatch<React.SetStateAction<AccountGetAllDto[]>>
}

export const AppUserContext = createContext<AppUserContextType>({
    appUser: undefined,
// eslint-disable-next-line @typescript-eslint/no-empty-function
    setAppUser: () =>{},

});

export const AccountsContext = createContext<AccountsContextType>({
    accounts: [],
    setAccounts: () =>{},
})
