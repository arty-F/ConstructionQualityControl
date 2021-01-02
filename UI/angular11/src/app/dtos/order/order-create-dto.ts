import { UserReadDto } from "../user/user-read-dto"

export class OrderCreateDto {
    user: UserReadDto
    demands: string
    isRoot: boolean
    subOrders: OrderCreateDto[]
}