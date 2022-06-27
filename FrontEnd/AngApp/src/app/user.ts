export interface User {
  id: number;
  name: string;
  surname: string;
  birthDate?: string;
  userTypeId: number;
  userTitleId: number;
  emailAddress: string;
  isActive: boolean;
  userTitleDesc:string;
  userTypeDesc:string;
  birth?: Date;
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

