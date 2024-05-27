export class Todo {
  constructor(data) {
    this.id = data.id
    this.title = data.title
    this.description = data.description
    this.completed = data.completed
    this.creatorId = data.creatorId
    this.creator = data.creator || {}
  }
}