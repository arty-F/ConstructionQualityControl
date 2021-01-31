import { CityReadDto } from "../city/city-read-dto"
import { CommentReadDto } from "../comment/comment-read-dto"
import { ReportReadDto } from "../report/report-read-dto"
import { UserReadDto } from "../user/user-read-dto"

export class OrderReadDto {
    id: number
    creationDate: Date
    prePaid: number
    postPaid: number
    user: UserReadDto
    demands: string
    isCompleted: boolean
    isStarted: boolean
    isRoot: boolean
    city: CityReadDto

    subOrders: OrderReadDto[]
    comments: CommentReadDto[]
    reports: ReportReadDto[]
}