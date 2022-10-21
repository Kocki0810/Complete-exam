<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreproduktgruppeRequest;
use App\Http\Requests\UpdateproduktgruppeRequest;
use App\Models\produktgruppe;

class ProduktgruppeController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        //
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \App\Http\Requests\StoreproduktgruppeRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(StoreproduktgruppeRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\produktgruppe  $produktgruppe
     * @return \Illuminate\Http\Response
     */
    public function show(produktgruppe $produktgruppe)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\produktgruppe  $produktgruppe
     * @return \Illuminate\Http\Response
     */
    public function edit(produktgruppe $produktgruppe)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\UpdateproduktgruppeRequest  $request
     * @param  \App\Models\produktgruppe  $produktgruppe
     * @return \Illuminate\Http\Response
     */
    public function update(UpdateproduktgruppeRequest $request, produktgruppe $produktgruppe)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\produktgruppe  $produktgruppe
     * @return \Illuminate\Http\Response
     */
    public function destroy(produktgruppe $produktgruppe)
    {
        //
    }
}
