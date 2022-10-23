<?php

namespace App\Http\Controllers\Api\V1;

use App\Http\Requests\StorefirmaRequest;
use App\Http\Requests\UpdatefirmaRequest;
use App\Models\firma;
use App\Http\Controllers\Controller;
use App\Http\Resources\V1\FirmaResource;
use App\Http\Resources\V1\FirmaCollection;

class FirmaController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return new firmaCollection(firma::paginate());
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
     * @param  \App\Http\Requests\StorefirmaRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(StorefirmaRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\firma  $firma
     * @return \Illuminate\Http\Response
     */
    public function show(firma $firma)
    {
        return new FirmaResource($firma);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\firma  $firma
     * @return \Illuminate\Http\Response
     */
    public function edit(firma $firma)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\UpdatefirmaRequest  $request
     * @param  \App\Models\firma  $firma
     * @return \Illuminate\Http\Response
     */
    public function update(UpdatefirmaRequest $request, firma $firma)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\firma  $firma
     * @return \Illuminate\Http\Response
     */
    public function destroy(firma $firma)
    {
        //
    }
}
