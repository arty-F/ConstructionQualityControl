import { UserReadDto } from "../user/user-read-dto"

export class OrderCreateDto {
    user: UserReadDto
    prePaid: number
    postPaid: number
    demands: string
    isRoot: boolean
    subOrders: OrderCreateDto[]
}