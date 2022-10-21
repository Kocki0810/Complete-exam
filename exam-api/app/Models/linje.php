<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class linje extends Model
{
    use HasFactory;

    public $table = "linjer";

    public function produkt() 
    {
        return $this->hasOne(produkt::class);
    }
    public function ordre() 
    {
        return $this->hasOne(ordre::class);
    }
}
