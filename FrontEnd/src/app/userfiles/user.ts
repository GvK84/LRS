export interface User {
  id: number;
  name: string;
  surname: string;
  birthDate?: Date;
  userTypeId: number;
  userTitleId: number;
  emailAddress: string;
  isActive: boolean;
}

export interface Title {
  id: number;
  description: string;
}

export interface Type {
  id: number;
  description: string;
  code: string;
}


