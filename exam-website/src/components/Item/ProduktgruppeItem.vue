<template>
<div>
    <div>
        <div class="form-group row">
            <input
            :value="this.navn"
            @input="m_ProduktNavn"
            type="text"
            placeholder="Navn"
            class="form-control col-sm font-weight-bold"
            />
            <button type="button" class="btn btn-danger col-md-auto m-2" @click="$emit('delete-produktgruppe', this.id, p_index)">Slet</button>
        </div>
    </div>
</div>
</template>

<script>
export default {
    props: {
        p_index: Number,
        p_produktgruppeID: Number,
        p_Navn: String,
  },
  data() {
    return {
        listIndex: this.p_index,
        id: this.p_produktgruppeID,
        navn: this.p_Navn,
    }
  },
  methods: {
        m_ProduktNavn () {
            this.$emit("produktgruppe-navn", this.p_index);
        },
    }
}
</script>

