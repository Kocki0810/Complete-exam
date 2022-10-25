<?php

namespace App\Http\Controllers\Api\V1;

use App\Http\Requests\StorelinjeRequest;
use App\Http\Requests\UpdatelinjeRequest;
use App\Models\linje;
use App\Http\Controllers\Controller;
use App\Http\Resources\V1\LinjeResource;
use App\Http\Resources\V1\LinjeCollection;

class LinjeController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return new linjeCollection(linje::paginate());
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
     * @param  \App\Http\Requests\StorelinjeRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(StorelinjeRequest $request)
    {
        $insertedRows = [];
        $data = collect($request->all());
        foreach($data as $column)
        {
            $newColumn = new LinjeResource(linje::create($column));
            $insertedRows[] = $newColumn->id;
        }

        return $insertedRows;
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function show(linje $linje)
    {
        return new LinjeResource($linje);
        
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function edit(linje $linje)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\UpdatelinjeRequest  $request
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function update(UpdatelinjeRequest $request, linje $linje)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function destroy(linje $linje)
    {
        $linje->Delete();
    }
}
