import { AppState } from "../AppState.js"
import { Todo } from "../models/Todo.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
class TodosService {

  async createTodo(newTodo) {
    const res = await api.post('api/todos', newTodo)
    AppState.todos.push(new Todo(res.data))
  }

  async getAllTodos() {
    const res = await api.get('api/todos')
    logger.log('List of Todos:', res.data)
    AppState.todos = res.data.map(t => new Todo(t))
  }
}

export const todosService = new TodosService()