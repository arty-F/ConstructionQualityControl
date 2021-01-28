import { CityReadDto } from "../city/city-read-dto"
import { UserReadDto } from "../user/user-read-dto"
import { WorkOfferReadDto } from "../work-offer/work-offer-read-dto"

export class WorkReadDto {
    id: number
    creationDate: Date
    prePaid: number
    postPaid: number
    user: UserReadDto
    demands: string
    isRoot: boolean
    city: CityReadDto
    subOrders: WorkReadDto[]
    workOffers: WorkOfferReadDto[]
}