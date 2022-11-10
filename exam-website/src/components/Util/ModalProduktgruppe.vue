<script>
export default {
  props: {
    p_show: Boolean
  }
}
</script>


<template>
  <Transition name="modal">
    <div v-if="p_show" class="modal-mask">
      <div class="form-group modal-wrapper">
        <div class="modal-container">
          <div class="modal-header">
            <slot name="header">default header</slot>
          </div>
          <div class="modal-body">
            <slot name="body">Vælg en handling:</slot>
          </div>
          
          <div class="modal-select">
            <span>Flyt alle produkterne til en anden gruppe:</span>
            <slot name="select"></slot><button class="btn btn-primary col-md-auto m-2" @click="$emit('Flyt-Produkter')">Flyt</button>
          </div>
          <br>
          <div> 
            <p>Eller slet alle produkterne sammen med gruppen</p>
            <button
            class="btn btn-danger col-md-auto m-2"
            type="button"
            @click="$emit('Delete-Produktgruppe')"
            >Slet</button>
            <button
            @click="$emit('close')"
            class="btn btn-primary col-md-auto m-2 modal-default-button"
            type="button"
            >Annuller</button>
          </div>
          <br>
        </div>
      </div>
    </div>
  </Transition>
</template>

<style>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}

.modal-container {
  width: 50%;
  margin: 0px auto;
  padding: 20px 30px;
  background-color: #fff;
  border-radius: 2px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.33);
  transition: all 0.3s ease;
}

.modal-header h3 {
  margin-top: 0;
  color: #42b983;
}

.modal-body {
  margin: 20px 0;
}

.modal-default-button {
  float: right;
}

/*
 * The following styles are auto-applied to elements with
 * transition="modal" when their visibility is toggled
 * by Vue.js.
 *
 * You can easily play with the modal transition by editing
 * these styles.
 */

.modal-enter-from {
  opacity: 0;
}

.modal-leave-to {
  opacity: 0;
}

.modal-enter-from .modal-container,
.modal-leave-to .modal-container {
  -webkit-transform: scale(1.1);
  transform: scale(1.1);
}
</style>