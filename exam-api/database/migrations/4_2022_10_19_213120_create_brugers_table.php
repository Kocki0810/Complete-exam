<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('brugere', function (Blueprint $table) {
            $table->increments('bruger_id')->unsigned();
            $table->integer('firma_id')->unsigned();
            $table->integer('ekspedient_id')->unsigned();
            $table->string('navn', 255);
            $table->string('username', 64);
            $table->string('password', 64);
            $table->timestamp('last_login');
            $table->string('last_IP', 255);
            $table->integer('adgangsgruppe')->unsigned();
            $table->string('email', 255);
            $table->string('telefon', 255);

            $table->softDeletes();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('brugere');
    }
};