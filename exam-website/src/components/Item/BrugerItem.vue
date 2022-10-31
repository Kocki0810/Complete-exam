<template>
<div>
    <div>
        <span>- ekspedient </span>
        <input
        :value="this.EksNavn"
        @input="m_EksNavn"
        type="navn"
        placeholder="Navn"
        />
        <input
        :value="this.EksKortnummer"
        @input="m_EksKortnummer"
        placeholder="Kortnummer"
        />
        <button v-if="p_Empty != 1" @click="SletEkspedient">Slet Ekspedient</button>
    </div>
    <div v-if="this.bruger?.showBruger != 0">
        <span>- Bruger </span>
        <input
        :value="this.bruger?.email"
        @input="m_Email"
        placeholder="Kortnummer"
        />
        <input
        :value="this.bruger?.password"
        @input="m_Password"
        placeholder="Navn"
        />
        <input
        :value="this.bruger?.telefon"
        @input="m_Telefon"
        placeholder="Telefon"
        />
        <button v-if="p_Empty != 1" @click="SletBruger">Slet Bruger</button>
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
            this.$emit("Delete-bruger", this.bruger.id, this.index);
        }
    }
}
</script>

