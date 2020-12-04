import { CityComponent } from 'src/app/city/city.component';

import { CityReadDto } from '../city/city-read-dto'

export class UserCreateDto {
    login: string
    password: string
    firstName: string
    lastName: string
    patronymic: string
    companyName: string
    companyDescription: string
    birthDate: Date
    city: CityReadDto
    role: string
}