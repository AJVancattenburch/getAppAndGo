import { AppState } from "../AppState.js"
import { Todo } from "../models/Todo.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
class TodosService {

  async createTodo(newTodo) {
    const res = await api.post('api/todos', newTodo)
    AppState.todos.unshift(new Todo(res.data))
  }

  async getAllTodos() {
    const res = await api.get('api/todos')
    logger.log('List of Todos:', res.data)
    const toDoList = res.data.map(t => new Todo(t))
    AppState.todos = toDoList
  }

  async getTodoById(todoId) {
    const res = await api.get('api/todos/' + todoId)
    AppState.activeTodo = new Todo(res.data)
  }

  async deleteTodo(todoId) {
    await api.delete('api/todos/' + todoId)
    AppState.todos = AppState.todos.filter(t => t.id !== todoId)

    logger.log(`Deleted Todo # ${todoId}`)
  }
}

export const todosService = new TodosService()