import { UserReadDto } from "../user/user-read-dto"

export class CommentReadDto {
    user: UserReadDto
    date: Date
    text: string
}