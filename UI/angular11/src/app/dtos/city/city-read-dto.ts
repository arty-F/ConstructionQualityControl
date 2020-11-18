import { RegionReadDto } from '../region/region-read-dto';

export class CityReadDto
{
    id: number;
    name: string;
    region: RegionReadDto;
}