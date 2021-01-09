import { UserReadDto } from "../user/user-read-dto"

export class OrderRootReadDto {
    id: number
    creationDate: Date
    user: UserReadDto
    demands: string
    isCompleted: boolean
    isStarted: boolean
}