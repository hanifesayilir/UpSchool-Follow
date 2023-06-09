export type LocalUser = {
  id: string;
  email: string;
  lastName: string;
  firstName: string;
  accessToken: string;
  expires: string;
};

export type AuthLoginCommand = {
  email: string;
  password: string;
};

export type LocalJwt = {
  accessToken: string;
  expires: string;
};
