<script setup>
import { ref } from "vue";
import { todosService } from "../../services/TodosService.js";
import { logger } from "../../utils/Logger.js";

const newTodo = ref({});

async function createTodo() {
  try {
    await todosService.createTodo(newTodo.value);
    logger.log(`Created Todo: ${newTodo.value.title}`);
    newTodo.value = {};
  } catch (error) {
    logger.error(error);
  }
}
</script>


<template>
  <form @submit.prevent="createTodo" class="form">
    <h2 class="title">Create a new Task:</h2>
    <div class="col-12 form-inputs d-flex align-items-center gap-1">
      <div class="col-3 input-container title-input mb-3">
        <input id="title" name="title" v-model="newTodo.title" type="text" placeholder=" " class="input" minLength="3" maxLength="50" required />
        <div class="cut"></div>
        <label for="title" class="placeholder">Title...</label>
      </div>
      <div class="col-9 input-container description-input mb-3">
        <input id="description" name="description" v-model="newTodo.description" type="text" placeholder=" " class="input" minLength="10" maxLength="250" required />
        <div class="cut-long"></div>
        <label for="description" class="placeholder">Description...</label>
      </div>
    </div>
    <button type="submit" class="btn btn-primary">Create</button>
  </form>
</template>

<style scoped lang="scss">
.form {
  background: radial-gradient(circle at left, #495159, #2E2E2E);
  border-radius: 20px;
  box-sizing: border-box;
  padding: 20px;
  transition: background 500ms;
  filter: 
    drop-shadow(0px 4px 4px #ffffff80)
    drop-shadow(0px 8px 8px #00000040)
    drop-shadow(8px 8px 8px #00000040);
    transition: filter 500ms;
  &:focus-within {
    .title {
      color: #222;
      text-shadow: 1px 1px 0.5px #000;
    }
    &::after {
      content: '';
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      color: #000;
      border: 2px solid #00000080;
      border-radius: 20px;
      background: radial-gradient(circle at bottom right, #94B0DA, #E2725B);
      background-origin: border-box;
      background-clip: padding-box, border-box;
      transition: background 500ms;
      z-index: -1;
      filter: 
        drop-shadow(0px 2px 2px #E2725B)
        drop-shadow(0px 3px 3px slateblue)
        drop-shadow(3px 3px 3px #94B0DA);
      opacity: 0;
      animation: fadeIn 500ms ease-in-out forwards;
      @keyframes fadeIn {
        0% {
          opacity: 0;
        }
        100% {
          opacity: 1;
        }
      }
    }
    .cut {
      filter: drop-shadow(0px 4px 4px #00000080);
    }
  }
}

.title {
  color: #AAA;
  font-size: 36px;
  font-weight: 600;
  margin-top: 30px;
}

.form-inputs {
  display: flex;
  gap: 5px;
}

.input-container {
  height: 50px;
  position: relative;
}

.title-input {
  margin-top: 20px;
}

.description-input {
  margin-top: 20px;
}

.input {
  background: radial-gradient(circle, rgba(0,0,0,0.125) 0%, rgba(0,0,0,0.2) 100%);
  border: none;
  outline: 1px solid currentColor;
  border-radius: 12px;
  box-sizing: border-box;
  color: #222;
  font-size: 18px;
  height: 100%;
  padding: 20px;
  width: 100%;
}

.cut, .cut-long {
  position: absolute;
  border-radius: 10px;
  height: 20px;
  left: 0px;
  top: -21px;
  transform: translateY(1000px);
  transition: transform 200ms;
}


.input:focus ~ .cut,
.input:not(:placeholder-shown) ~ .cut,
.input:focus ~ .cut-long,
.input:not(:placeholder-shown) ~ .cut-long {
  transform: translateY(-1px);
}

.placeholder {
  position: absolute;
  left: 20px;
  line-height: 14px;
  pointer-events: none;
  background: none;
  transform-origin: 0 50%;
  transition: transform 200ms, color 200ms;
  top: 20px;
}

.input:focus ~ .placeholder,
.input:not(:placeholder-shown):invalid ~ .placeholder {
  color: #E2725B;
  transform: translateY(-41px) translateX(-13px) scale(0.75);
}

input:not(:placeholder-shown) {
  &:valid::after {
    outline: 1px ridge green;
  }
  &:invalid::after {
    outline: 0.5px dashed #E2725B;
  }
}

input:placeholder-shown ~ .placeholder {
  -webkit-text-fill-color: #DDD;
}

input:not(:placeholder-shown) ~ .placeholder {
  color: #000;
}

.input:focus ~ .placeholder, label {
  color: #000;
  -webkit-text-fill-color: #000;
  text-shadow: 1px 1px 0.5px #000;
}

.form .input-container > label {
  -webkit-text-fill-color: #DDD;
}
.form:focus-within .input-container > label {
  -webkit-text-fill-color: #000;
}

input.title-input {
  border: 2px solid #000;
  text-align: center !important;
  transform: translateX(1000px)
}

input:not(:placeholder-shown):valid ~ .placeholder {
  transform: translateY(-41px) translateX(-13px) scale(0.75);
  visibility: hidden;
  &::before {
    content: 'Confirmed ✓';
    position: absolute;
    height: 100%;
    width: 100%;
    color: green;
    font-size: 12px;
    font-weight: 700;
    white-space: nowrap;
    display: block;
    visibility: visible;
  }
}
</style>