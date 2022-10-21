<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class produktgruppe extends Model
{
    use HasFactory;

    public $table = "produktgrupper";

    public function produkter() 
    {
        return $this->hasMany(produkt::class);
    }
    public function firma() 
    {
        return $this->hasOne(firma::class);
    }
}
