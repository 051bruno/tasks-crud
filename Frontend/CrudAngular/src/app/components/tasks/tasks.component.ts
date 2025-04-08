import { Component, OnInit } from '@angular/core';
import { TasksService } from './services/tasks.service';
import { TaskModel } from './Models/TaskModel';
import { NgIf, NgFor, CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, NgIf, NgFor],
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})

export class TasksComponent implements OnInit {

  // #region VARIAVEIS
  public tasks: TaskModel[] = [];
  public task?: TaskModel;
  // public retTasks: RetTaskModel[] = []; //Será usada para adicionar um spinner enquanto os dados são carregados.
  public isModalOpen: boolean = false;
  public editingMode: boolean = false;

  public taskForm : FormGroup = new FormGroup({});
  // #endregion VARIAVEIS

  // #region FORM
  private createBaseForm() {
    this.taskForm = this._formBuilder.group({
      txNomeTask: ['', [Validators.required, Validators.maxLength(100)]],
      txDescricaoTask: ['', [Validators.required, Validators.maxLength(200)]],
      isConcluida: [false]
    });
  }
  // #region FORM

  constructor(private tasksService: TasksService, private _formBuilder: FormBuilder) {
    
  }

  ngOnInit() {
    this.loadTasks();
    this.createBaseForm();
  }
  
  // #region HTTP METHODS

  // GET LIST Carrega todas as tarefas
  loadTasks() {
    this.tasksService.getTasks().subscribe({
      next: response => {
        this.tasks = response.data;  // `data` contém a lista de tarefas
      },
      error: err => {
        console.error('Erro ao buscar tarefas:', err);
      }
    });
  }

  //método GET
  private get(id: number | undefined) {
    this.tasksService.getTaskById(id).subscribe({
      next: response => {
        this.task = response;  
        this.taskForm = this._formBuilder.group({
          txNomeTask: [this.task.nomeTask, [Validators.required, Validators.maxLength(100)]],
          txDescricaoTask: [this.task.descricaoTask, [Validators.required, Validators.maxLength(200)]],
          isConcluida: [this.task.isTaskConcluida] 
        });
      }
    });
  }

  //Create
  private createTask(task: TaskModel): void {
    this.tasksService.createTask(task).subscribe({
      next: (response) => {
        alert('Tarefa criada com sucesso!');
        this.loadTasks();
        this.closeModal(); // Fecha o modal após a criação
      },
      error: () => {
        alert('Erro ao criar tarefa!');
      }
    });
  }
  
  // UPDATE
  private updateTask(task: TaskModel): void {
    if (!task.id) {
      alert('Erro: ID da tarefa não encontrado!');
      return;
    }
  
    this.tasksService.updateTask(task.id, task).subscribe({
      next: (response) => {
        alert('Tarefa atualizada com sucesso!');
        this.loadTasks();
        this.closeModal(); // Fecha o modal após a atualização
      },
      error: () => {
        alert('Erro ao atualizar tarefa!');
      }
    });
  }
  
  // Marca tarefa da listagem como concluída
  toggleComplete(task: TaskModel) {
    task.isTaskConcluida = !task.isTaskConcluida;
    this.tasksService.updateTask(task.id!, task).subscribe(() => {
      this.loadTasks(); // Atualiza a lista após a edição
    });
  }
  
  // Excluir uma tarefa
  deleteTask(id: number) {
    this.tasksService.deleteTask(id).subscribe(() => {
      alert('Tarefa exclúida com sucesso!')
      this.loadTasks();
    });
  }
  // #endregion HTTP METHODS

  // #region UTILITIES
  openModal(editing: boolean, id?: number) {    
    this.editingMode = editing;  // Define o modo de edição diretamente aqui
    if (editing) {
      this.get(id); // Chama a função de edição, passando o id
    } else {
      this.createBaseForm(); // Chama a função de criação
    }
    this.isModalOpen = true;  // Abre o modal
  }

  closeModal() {
    this.isModalOpen = false;
    this.editingMode = false;
    this.createBaseForm();
  }

  public saveTask(): void {
    const task = this.assignValues();
  
    if (!task) {
      alert('Preencha todos os campos corretamente!');
      return;
    }
    
    if (this.editingMode) {
      // Se estiver editando, chama updateTask()
      this.updateTask(task);
    } else {
      // Caso contrário, cria uma nova tarefa
      this.createTask(task);
    }
  }

  assignValues(): TaskModel | null {
    // Verifica se o formulário é válido
    if (this.taskForm.invalid) {
      return null;  // Se o formulário for inválido, retorna null
    }
  
    // Cria a tarefa com os valores do formulário
    const task: TaskModel = {
      id : this.editingMode && this.task ? this.task.id : 0,
      nomeTask: this.taskForm.get('txNomeTask')?.value,
      descricaoTask: this.taskForm.get('txDescricaoTask')?.value,
      isTaskConcluida: this.taskForm.get('isConcluida')?.value
    };
    
    console.log(task.id)
    return task;  // Retorna a tarefa criada se os dados forem válidos
  }
  // #endregion UTILITIES

}


