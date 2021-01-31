import { OrderReadDto } from "../order/order-read-dto"
import { UserReadDto } from "../user/user-read-dto"

export class CommentCreateDto {
    user: UserReadDto
    order: OrderReadDto
    text: string
}