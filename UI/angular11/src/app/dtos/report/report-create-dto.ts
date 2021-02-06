import { OrderReadDto } from "../order/order-read-dto"
import { UserReadDto } from "../user/user-read-dto"

export class ReportCreateDto {
    user: UserReadDto
    order: OrderReadDto
    data: string
}