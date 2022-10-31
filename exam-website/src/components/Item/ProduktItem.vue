<template>
    <div>
        <div>
            <input
            :value="this.navn"
            @input="m_ProduktNavn"
            type="navn"
            placeholder="Navn"
            />
            <input
            :value="this.pris"
            @input="m_Produktpris"
            type="navn"
            placeholder="Pris"
            />
            <select @change="m_Produktgruppe">
                <option selected>Flyt Produktgruppe</option>
                <option v-for="(produktgruppe, index) in p_Produktgruppe" v-bind:key="produktgruppe?.id"
                :value="produktgruppe?.id + ',' + index"
                @input="m_Produktpris"
                type="navn"
                placeholder="Pris">{{produktgruppe.navn}}
                </option>
            </select>
            <button @click="$emit('delete-produkt', p_ProduktID, p_Index, p_GruppeIndex)">Slet</button>
        </div>
    </div>
</template>


<script>

export default {
    props: {
        p_Index: Number,
        p_GruppeIndex: Number,
        p_ProduktID: Number,
        p_Navn: String,
        p_Pris: Number,
        p_Produktgruppe_id: String,
        p_Produktgruppe: Array
  },
  data() {
    return {
        produktgrupper: this.p_Produktgruppe,
        navn: this.p_Navn,
        pris: this.p_Pris,
        gruppeNavn: this.p_Produktgruppe?.navn ?? "",
        produktgruppe_id: this.p_Produktgruppe_id
    }
  },
  methods: {
        m_ProduktNavn () {
            this.$emit("produkt-navn", this.p_Index, this.p_GruppeIndex, "navn");
        },
        m_Produktpris () {
            this.$emit("produkt-navn", this.p_Index, this.p_GruppeIndex, "pris");
        },
        m_Produktgruppe () {
            if(this.p_ProduktID == -1)
            {
                this.produktgruppe_id = event.target.value;
            }
            else
            {
                this.$emit("produkt-navn", this.p_Index, this.p_GruppeIndex, "gruppe");
            }
        }
    }
}
</script>

