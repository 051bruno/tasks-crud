import { TaskModel } from './TaskModel';

export class RetTaskModel {
  error: boolean;
  errorMessage: string;
  data: TaskModel[];

  constructor(
    error: boolean = false,
    errorMessage: string = '',
    data: TaskModel[] = []
  ) {
    this.error = error;
    this.errorMessage = errorMessage;
    this.data = data;
  }
}
