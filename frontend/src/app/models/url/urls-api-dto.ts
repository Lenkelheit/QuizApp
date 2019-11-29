import { UrlDto } from './url-dto';

export interface UrlsApiDto {
    totalCount: number;

    urls: UrlDto[];
}
