import { RegionReadDto } from '../region/region-read-dto';

export class CityReadDto
{
    Id: number;
    Name: string;
    Region: RegionReadDto;
}