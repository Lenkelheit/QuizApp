import { UserLoggedinDto } from './user-loggedin-dto';

export interface UserAuthenticationResultDto {
    userLoggedin: UserLoggedinDto;
    isValid: boolean;

    errors: string[];
}
