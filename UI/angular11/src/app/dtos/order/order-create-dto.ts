import { UserReadDto } from "../user/user-read-dto"

export class OrderCreateDto {
    user: UserReadDto
    price: number
    demands: string
    isRoot: boolean
    subOrders: OrderCreateDto[]
}