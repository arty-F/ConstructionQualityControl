import { UserReadDto } from "../user/user-read-dto"

export class ReportReadDto {
    user: UserReadDto
    creationDate: Date
    data: string
}