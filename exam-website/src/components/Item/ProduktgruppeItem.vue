<template>
<div>
    <div>
        <input
        :value="this.navn"
        @input="m_ProduktNavn"
        type="navn"
        placeholder="Navn"
        />
        <button @click="$emit('delete-produktgruppe', this.id, p_index)">Slet</button>
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

