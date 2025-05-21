import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  todos: string[] = ['Einkaufen', 'E-Mails beantworten', 'Workout'];

  deleteTodo(todo: string): void {
    this.todos = this.todos.filter(t => t !== todo);
  }}
