import { UserReadDto } from "../user/user-read-dto"

export class AuthResult {
    access_Token: string
    user: UserReadDto
}