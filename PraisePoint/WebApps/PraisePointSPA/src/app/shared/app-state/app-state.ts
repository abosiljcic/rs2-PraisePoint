import { Role } from './role';

export interface IAppState {
  accessToken?: string;
  refreshToken?: string;
  username?: string;
  email?: string;
  roles?: Role | Role[];
  firstName?: string;
  lastName?: string;
  userId?: string;
  password?: string;
  phoneNumber?: string;
  companyId?: string;
  imageUrl?: string;

  hasRole(role: Role): boolean;
  clone(): IAppState;
  isEmpty(): boolean;
}

export class AppState implements IAppState {
  public accessToken?: string;
  public refreshToken?: string;
  public username?: string;
  public email?: string;
  public roles?: Role | Role[];
  public firstName?: string;
  public lastName?: string;
  public userId?: string;
  public password?: string;
  public phoneNumber?: string;
  public companyId?: string;
  public imageUrl?: string;

  public constructor();
  public constructor(
    accessToken?: string,
    refreshToken?: string,
    username?: string,
    email?: string,
    roles?: Role | Role[],
    firstName?: string,
    lastName?: string,
    userId?: string,
    password?: string,
    phoneNumber?: string,
    companyId?: string,
    imageUrl?: string
  );

  public constructor(...args: any[]) {
    if (args.length === 0) {
      return;
    } else if (args.length === 12) {
      this.accessToken = args[0];
      this.refreshToken = args[1];
      this.username = args[2];
      this.email = args[3];
      this.roles = args[4];
      this.firstName = args[5];
      this.lastName = args[6];
      this.userId = args[7];
      this.password = args[8];
      this.phoneNumber = args[9];
      this.companyId = args[10];
      this.imageUrl = args[11];
    }
  }

  public hasRole(role: Role): boolean {
    if (!this.roles) {
      return false;
    }
    if (typeof this.roles === 'string') {
      return this.roles === role;
    }
    return this.roles.find((registeredRole: Role) => registeredRole === role) !== undefined;
  }

  public clone(): IAppState {
    const newState = new AppState();
    Object.assign(newState, this);
    return newState;
  }

  public isEmpty(): boolean {
    return this.accessToken === undefined /* && this.refreshToken === undefined && ... */;
  }
}