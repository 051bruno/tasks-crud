export class TaskModel {
  id: number; // O id pode ser opcional
  nomeTask: string;
  descricaoTask: string;
  isTaskConcluida: boolean;

  constructor(
    id: number,
    nomeTask: string,
    descricaoTask: string,
    isTaskConcluida: boolean,
  ) {
    this.id = id;
    this.nomeTask = nomeTask;
    this.descricaoTask = descricaoTask;
    this.isTaskConcluida = isTaskConcluida;
  }
}
