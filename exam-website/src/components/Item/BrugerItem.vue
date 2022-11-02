<template>
<div>
    <div class="form-group row m-2">
        <input
        :value="this.EksNavn"
        @input="m_EksNavn"
        type="navn"
        placeholder="Navn"
        class="form-control col-sm"
        />
        <input
        :value="this.EksKortnummer"
        @input="m_EksKortnummer"
        placeholder="Kortnummer"
        class="form-control col-sm"
        />
        <button type="button" class="btn btn-primary col-md-auto m-2" @click="SletEkspedient">Slet Ekspedient</button>
    </div>
    <div class="form-group row m-2">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="max" fill="currentColor" class="bi bi-dot col-md-auto" viewBox="0 0 16 16">
        <path d="M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z"/>
        </svg>
        <input
        :value="this.bruger?.email"
        @input="m_Email"
        placeholder="Kortnummer"
        class="form-control col-sm"
        />
        <input
        :value="this.bruger?.password"
        @input="m_Password"
        placeholder="Navn"
        class="form-control col-sm"
        />
        <input
        :value="this.bruger?.telefon"
        @input="m_Telefon"
        placeholder="Telefon"
        class="form-control col-sm"
        />
        <button type="button" class="btn btn-primary col-md-auto m-2"  @click="SletBruger">Slet Bruger</button>
    </div>
</div>
</template>

<script>

export default {
    props: {
        p_Empty: Number,
        p_Index: Number,
        p_Bruger: Array,
        p_Eks_ID: Number,
        p_Eks_Navn: String,
        p_Eks_Kortnummer: String,
    },
    data() {
        return {
            index: this.p_Index,
            bruger: this.p_Bruger,
            EksNavn: this.p_Eks_Navn,
            EksKortnummer: this.p_Eks_Kortnummer,
            EksID: this.p_Eks_ID
        }
  },
  methods: {
        m_EksNavn () {
            this.$emit("Ekspedient-Input", this.index, "eks", "navn");
        },
        m_EksKortnummer () {
            this.$emit("Ekspedient-Input", this.index, "eks", "kortnummer");
        },
        m_Email () {
            this.$emit("Ekspedient-Input", this.index, "bruger", "email");
        },
        m_Password () {
            this.$emit("Ekspedient-Input", this.index, "bruger", "password");
        },
        m_Telefon () {
            this.$emit("Ekspedient-Input", this.index, "bruger", "telefon");
        },
        SletEkspedient()
        {
            this.$emit("Delete-ekspedient", this.EksID, this.index);
        },
        SletBruger()
        {
            this.$emit("Delete-bruger", this.index);
        }
    }
}
</script>

