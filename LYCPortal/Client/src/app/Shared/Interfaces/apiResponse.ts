import { ResponseStatus } from "../Enums/enum";

export interface ApiResponse {
  status: ResponseStatus;
  message: string;
  resultData: object;
}
