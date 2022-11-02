<template>
    <div>
        <div>
            <div class="form-group row m-2">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="max" fill="currentColor" class="bi bi-dot col-md-auto" viewBox="0 0 16 16">
                <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z"/>
                </svg>
                <input
                :value="this.navn"
                @input="m_ProduktNavn"
                type="navn"
                placeholder="Navn"
                class="form-control col-sm"
                />
                <input
                :value="this.pris"
                @input="m_Produktpris"
                type="navn"
                placeholder="Pris"
                class="form-control col-sm"
                />
                <select @change="m_Produktgruppe" class="form-control col-sm">
                    <option selected>Flyt Produktgruppe</option>
                    <option v-for="(produktgruppe) in p_Produktgruppe" v-bind:key="produktgruppe?.id"
                    :value="produktgruppe?.id"
                    type="navn"
                    placeholder="Pris">{{produktgruppe.navn}}
                </option>
                </select>
                <button type="button" class="btn btn-danger col-md-auto m-2" @click="$emit('delete-produkt', p_ProduktID, p_Index, p_GruppeIndex)">Slet</button>
            </div>
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

