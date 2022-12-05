export class PagedResultDto<T> {
  totalCount: number = 0
  items: T | undefined;
}
