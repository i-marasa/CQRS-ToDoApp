import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TodoService } from '../todo.service';
import { Todo } from '../todo.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-todo',
  standalone: true,
  imports: [RouterModule, ReactiveFormsModule, CommonModule],
  templateUrl: './edit-todo.component.html',
  styleUrls: ['./edit-todo.component.css']
})
export class EditTodoComponent implements OnInit {
  todoForm: FormGroup;
  todoId: number = 0;

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private todoService: TodoService, private router: Router) {
    this.todoForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(5)]],
      completed: [false]
    });
  }

  ngOnInit(): void {
    this.todoId = +this.route.snapshot.paramMap.get('id')!;
    this.todoService.getTodos().subscribe({
      next: (todos) => {
        const foundTodo = todos.find(todo => todo.id === this.todoId);
        if (foundTodo) {
          this.todoForm.patchValue(foundTodo);
        }
      },
      error: (error) => console.error('Error loading todo:', error)
    });
  }

  onSubmit(): void {
    if (this.todoForm.valid) {
      const updatedTodo = { ...this.todoForm.value, id: this.todoId };
      this.todoService.updateTodo(updatedTodo).subscribe({
        next: () => this.router.navigate(['/todos']),
        error: (error) => console.error('Error updating todo:', error)
      });
    }
  }
}
