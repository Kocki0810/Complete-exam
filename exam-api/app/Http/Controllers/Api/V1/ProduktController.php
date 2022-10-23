<?php

namespace App\Http\Controllers\Api\V1;

use App\Http\Requests\StoreproduktRequest;
use App\Http\Requests\UpdateproduktRequest;
use App\Models\produkt;
use App\Http\Controllers\Controller;
use App\Http\Resources\V1\ProduktResource;
use App\Http\Resources\V1\ProduktCollection;

class ProduktController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return new ProduktCollection(produkt::paginate());
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
     * @param  \App\Http\Requests\StoreproduktRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(StoreproduktRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\produkt  $produkt
     * @return \Illuminate\Http\Response
     */
    public function show(produkt $produkt)
    {
        return new ProduktResource($produkt);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\produkt  $produkt
     * @return \Illuminate\Http\Response
     */
    public function edit(produkt $produkt)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\UpdateproduktRequest  $request
     * @param  \App\Models\produkt  $produkt
     * @return \Illuminate\Http\Response
     */
    public function update(UpdateproduktRequest $request, produkt $produkt)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\produkt  $produkt
     * @return \Illuminate\Http\Response
     */
    public function destroy(produkt $produkt)
    {
        //
    }
}
